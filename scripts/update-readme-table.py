#!/usr/bin/env python3
"""
Regenerate the "## Progress" table in the top-level README.md.

Scans every problem folder under src/ for solution files, pulls each
problem's Topic/Difficulty (if its own README.md lists them), and writes a
markdown table between the <!-- progress:start --> / <!-- progress:end -->
markers in the top-level README.md. Everything outside those markers is
left untouched — if the markers aren't present yet, a new "## Progress"
section is inserted before "## Testing locally" (or appended at the end).

Usage:
  ./scripts/update-readme-table.py
"""

from __future__ import annotations

import re
import subprocess
from pathlib import Path

# (file extension, emoji, language name) — order controls column order and
# legend order everywhere below.
LANGUAGES = [
    ("py", "🐍", "Python"),
    ("js", "🟨", "JavaScript"),
    ("ts", "🔷", "TypeScript"),
    ("go", "🐹", "Go"),
    ("cs", "🟪", "C#"),
    ("java", "☕", "Java"),
    ("kt", "🟠", "Kotlin"),
    ("rb", "💎", "Ruby"),
]

START_MARKER = "<!-- progress:start -->"
END_MARKER = "<!-- progress:end -->"
ANCHOR_HEADING = "## Testing locally"


def repo_root() -> Path:
    out = subprocess.run(
        ["git", "rev-parse", "--show-toplevel"],
        capture_output=True,
        text=True,
        check=True,
    )
    return Path(out.stdout.strip())


def extract_field(text: str, field: str) -> str | None:
    """Find a `Field: value` line, case-insensitive, ignoring bold markers
    (so `**Topic:** X`, `**Topic**: X`, and plain `Topic: X` all match)."""
    for line in text.splitlines():
        unbolded = line.strip().replace("**", "")
        m = re.match(rf"^{re.escape(field)}\s*:\s*(.+?)\s*$", unbolded, re.IGNORECASE)
        if m:
            return m.group(1).strip()
    return None


def title_from_folder(folder: str) -> str:
    return " ".join(w.capitalize() for w in folder.split("-"))


def problem_row(folder: Path) -> tuple[str, str, str, str, str]:
    """Returns (sort_key, problem cell, topic, difficulty, solutions cell)."""
    readme = folder / "README.md"
    text = readme.read_text(encoding="utf-8") if readme.exists() else ""

    heading_match = re.match(r"^#\s+(.+?)\s*$", text, re.MULTILINE)
    title = heading_match.group(1) if heading_match else title_from_folder(folder.name)

    topic = extract_field(text, "Topic") or "—"
    difficulty = extract_field(text, "Difficulty") or "—"

    emojis = [
        emoji
        for ext, emoji, _name in LANGUAGES
        if (folder / f"solution.{ext}").exists()
    ]
    solutions = " ".join(emojis) if emojis else "—"

    problem_cell = f"[{title}](src/{folder.name})"
    return (title.lower(), problem_cell, topic, difficulty, solutions)


def build_progress_block(src_dir: Path) -> str:
    folders = sorted(p for p in src_dir.iterdir() if p.is_dir())
    rows = sorted((problem_row(f) for f in folders), key=lambda r: r[0])

    legend = " · ".join(f"{emoji} {name}" for _ext, emoji, name in LANGUAGES)

    lines = [
        f"Legend: {legend}",
        "",
        "| Problem | Topic | Difficulty | Solutions |",
        "| --- | --- | --- | --- |",
    ]
    for _key, problem_cell, topic, difficulty, solutions in rows:
        lines.append(f"| {problem_cell} | {topic} | {difficulty} | {solutions} |")

    return "\n".join(lines)


def render_section(block: str) -> str:
    return f"## Progress\n\n{START_MARKER}\n{block}\n{END_MARKER}"


def update_readme(readme_path: Path, block: str) -> None:
    content = readme_path.read_text(encoding="utf-8")

    if START_MARKER in content and END_MARKER in content:
        pattern = re.compile(
            re.escape(START_MARKER) + r".*?" + re.escape(END_MARKER),
            re.DOTALL,
        )
        new_content = pattern.sub(f"{START_MARKER}\n{block}\n{END_MARKER}", content, count=1)
    else:
        section = render_section(block)
        anchor = f"{ANCHOR_HEADING}\n"
        if anchor in content:
            new_content = content.replace(anchor, f"{section}\n\n{anchor}", 1)
        else:
            sep = "\n\n" if content and not content.endswith("\n\n") else ""
            new_content = content.rstrip("\n") + "\n\n" + section + "\n"

    if new_content != content:
        readme_path.write_text(new_content, encoding="utf-8")


def main() -> None:
    root = repo_root()
    src_dir = root / "src"
    if not src_dir.is_dir():
        return

    block = build_progress_block(src_dir)
    update_readme(root / "README.md", block)


if __name__ == "__main__":
    main()

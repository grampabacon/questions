#!/usr/bin/env bash
#
# Format every solution file in the repo, in place, using each language's
# standard formatter. Skips a language silently (with an install hint) if
# its formatter isn't on PATH — mirrors scripts/run-all.sh's skip pattern.
#
# Usage:
#   ./scripts/format.sh

set -uo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(dirname "$SCRIPT_DIR")"
cd "$REPO_ROOT"

RAN_ANY=0

# --- Python: black ---
if command -v black >/dev/null 2>&1; then
  FILES=$(find . -name "solution.py")
  if [ -n "$FILES" ]; then
    echo ">> Formatting Python (black)"
    echo "$FILES" | xargs black -q
    RAN_ANY=1
  fi
else
  [ -n "$(find . -name 'solution.py')" ] && echo "(skipping Python — black not found; install with: pip install black --break-system-packages)"
fi

# --- JavaScript + TypeScript: prettier ---
if command -v prettier >/dev/null 2>&1 || npx --no-install prettier --version >/dev/null 2>&1; then
  FILES=$(find . -name "solution.js" -o -name "solution.ts")
  if [ -n "$FILES" ]; then
    echo ">> Formatting JavaScript/TypeScript (prettier)"
    echo "$FILES" | xargs npx --no-install prettier --write
    RAN_ANY=1
  fi
else
  MATCHES=$(find . -name "solution.js" -o -name "solution.ts")
  [ -n "$MATCHES" ] && echo "(skipping JS/TS — prettier not found; install with: npm install -g prettier)"
fi

# --- Go: gofmt (ships with the Go toolchain) ---
if command -v gofmt >/dev/null 2>&1; then
  FILES=$(find . -name "solution.go")
  if [ -n "$FILES" ]; then
    echo ">> Formatting Go (gofmt)"
    echo "$FILES" | xargs gofmt -w
    RAN_ANY=1
  fi
else
  [ -n "$(find . -name 'solution.go')" ] && echo "(skipping Go — gofmt not found; comes with the Go toolchain: https://go.dev/dl)"
fi

# --- C#: csharpier ---
if command -v csharpier >/dev/null 2>&1; then
  FILES=$(find . -name "solution.cs")
  if [ -n "$FILES" ]; then
    echo ">> Formatting C# (csharpier)"
    echo "$FILES" | xargs -I{} csharpier format {}
    RAN_ANY=1
  fi
else
  [ -n "$(find . -name 'solution.cs')" ] && echo "(skipping C# — csharpier not found; install with: dotnet tool install -g csharpier)"
fi

# --- Java: google-java-format ---
# Downloaded once into .tools/ (gitignored) since there's no simple global
# package-manager install for it, unlike the others.
GJF_DIR="$REPO_ROOT/.tools"
GJF_JAR="$GJF_DIR/google-java-format.jar"
GJF_VERSION="1.36.1"
GJF_URL="https://github.com/google/google-java-format/releases/download/v${GJF_VERSION}/google-java-format-${GJF_VERSION}-all-deps.jar"

if command -v java >/dev/null 2>&1; then
  FILES=$(find . -name "solution.java")
  if [ -n "$FILES" ]; then
    if [ ! -f "$GJF_JAR" ]; then
      mkdir -p "$GJF_DIR"
      echo "Downloading google-java-format (one-time)..."
      curl -sL "$GJF_URL" -o "$GJF_JAR" || true
      if [ ! -s "$GJF_JAR" ] || [ "$(head -c 2 "$GJF_JAR" 2>/dev/null)" != "PK" ]; then
        rm -f "$GJF_JAR"
        echo "(skipping Java — download failed or was blocked; get the jar manually from"
        echo " https://github.com/google/google-java-format/releases and save it to $GJF_JAR)"
        FILES=""
      fi
    fi
    if [ -n "$FILES" ]; then
      echo ">> Formatting Java (google-java-format)"
      echo "$FILES" | xargs java -jar "$GJF_JAR" --replace
      RAN_ANY=1
    fi
  fi
else
  [ -n "$(find . -name 'solution.java')" ] && echo "(skipping Java — java not found)"
fi

# --- Kotlin: ktlint ---
if command -v ktlint >/dev/null 2>&1; then
  FILES=$(find . -name "solution.kt")
  if [ -n "$FILES" ]; then
    echo ">> Formatting Kotlin (ktlint)"
    echo "$FILES" | xargs ktlint -F
    RAN_ANY=1
  fi
else
  [ -n "$(find . -name 'solution.kt')" ] && echo "(skipping Kotlin — ktlint not found; install with: brew install ktlint)"
fi

echo ""
if [ "$RAN_ANY" -eq 1 ]; then
  echo "Formatting complete."
else
  echo "Nothing was formatted — no matching tools were available."
fi

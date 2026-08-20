#!/usr/bin/env bash
#
# Run every solution file in the repo, across every problem and language.
#
# Usage:
#   ./scripts/run-all.sh

set -uo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(dirname "$SCRIPT_DIR")"
cd "$REPO_ROOT"

FAIL=0
COUNT=0

run_group() {
  local label="$1"
  local filename="$2"
  local cmd="$3"
  local checker="$4"

  if ! command -v "$checker" >/dev/null 2>&1; then
    local matches
    matches=$(find . -name "$filename" 2>/dev/null)
    if [ -n "$matches" ]; then
      echo "(skipping $label — '$checker' not found on PATH)"
    fi
    return
  fi

  while IFS= read -r -d '' file; do
    COUNT=$((COUNT + 1))
    echo ""
    echo ">> $label: $file"
    if eval "$cmd \"$file\""; then
      echo "   PASS"
    else
      echo "   FAIL"
      FAIL=1
    fi
  done < <(find . -name "$filename" -print0 2>/dev/null)
}

echo "Running all solutions in $REPO_ROOT"
echo "===================================="

run_group "Python"     "solution.py"   "python3"       "python3"
run_group "JavaScript" "solution.js"   "node"          "node"
run_group "TypeScript" "solution.ts"   "tsx"           "tsx"
run_group "Go"         "solution.go"   "go run"        "go"
run_group "C#"         "solution.cs"   "dotnet-script" "dotnet-script"
run_group "Java"       "solution.java" "java"          "java"

echo ""
echo "===================================="
if [ "$COUNT" -eq 0 ]; then
  echo "No solution files found."
  exit 1
elif [ "$FAIL" -eq 0 ]; then
  echo "All $COUNT solution(s) passed."
  exit 0
else
  echo "Some solutions failed. See FAIL lines above."
  exit 1
fi

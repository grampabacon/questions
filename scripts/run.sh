#!/usr/bin/env bash
#
# Run every solution file for ONE problem.
#
# Usage:
#   ./scripts/run.sh two-sum/
#
# Skips a language silently if that toolchain isn't installed locally.

set -uo pipefail

if [ $# -eq 0 ]; then
  echo "Usage: $0 <problem-folder>"
  echo "Example: $0 two-sum"
  exit 1
fi

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(dirname "$SCRIPT_DIR")"
DIR="${1%/}"
DIR="${DIR#./}"

TARGET="$REPO_ROOT/src/$DIR"
if [ ! -d "$TARGET" ]; then
  if [ -d "$DIR" ]; then
    TARGET="$DIR"
  else
    echo "No such problem folder: $DIR"
    exit 1
  fi
fi

echo "Running solutions in $TARGET"
echo "----------------------------------------"

FAIL=0
RAN_ANY=0

run_one() {
  local label="$1"
  local cmd="$2"
  local file="$3"

  if [ -f "$file" ]; then
    RAN_ANY=1
    echo ""
    echo ">> $label ($file)"
    if eval "$cmd \"$file\""; then
      echo "   PASS"
    else
      echo "   FAIL"
      FAIL=1
    fi
  fi
}

if command -v python3 >/dev/null 2>&1; then
  run_one "Python" "python3" "$TARGET/solution.py"
else
  [ -f "$TARGET/solution.py" ] && echo "(skipping Python — python3 not found)"
fi

if command -v node >/dev/null 2>&1; then
  run_one "JavaScript" "node" "$TARGET/solution.js"
else
  [ -f "$TARGET/solution.js" ] && echo "(skipping JavaScript — node not found)"
fi

if command -v go >/dev/null 2>&1; then
  run_one "Go" "go run" "$TARGET/solution.go"
else
  [ -f "$TARGET/solution.go" ] && echo "(skipping Go — go not found)"
fi

if command -v dotnet-script >/dev/null 2>&1; then
  run_one "C#" "dotnet-script" "$TARGET/solution.cs"
elif [ -f "$TARGET/solution.cs" ]; then
  echo "(skipping C# — dotnet-script not found; install with: dotnet tool install -g dotnet-script)"
fi

echo ""
echo "----------------------------------------"
if [ "$RAN_ANY" -eq 0 ]; then
  echo "No solution files found in $TARGET"
  exit 1
elif [ "$FAIL" -eq 0 ]; then
  echo "All solutions passed."
  exit 0
else
  echo "One or more solutions failed."
  exit 1
fi

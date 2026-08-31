#!/usr/bin/env bash
#
# Run every solution file for ONE problem.
#
# Usage:
#   ./scripts/run.sh two-sum/

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

# kotlin(1) only runs .jar/.kts/qualified-class targets, not a plain .kt
# file — compile it to a throwaway jar with kotlinc, run that, clean up.
run_kotlin() {
  local file="$1"
  local jar
  jar="$(mktemp -t kotlin-solution-XXXXXX).jar"
  if kotlinc "$file" -include-runtime -d "$jar" 2>/dev/null; then
    java -jar "$jar"
    local status=$?
    rm -f "$jar"
    return $status
  fi
  rm -f "$jar"
  return 1
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

if command -v tsx >/dev/null 2>&1; then
  run_one "TypeScript" "tsx" "$TARGET/solution.ts"
else
  [ -f "$TARGET/solution.ts" ] && echo "(skipping TypeScript — tsx not found; install with: npm install -g tsx)"
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

if command -v java >/dev/null 2>&1; then
  run_one "Java" "java" "$TARGET/solution.java"
else
  [ -f "$TARGET/solution.java" ] && echo "(skipping Java — java not found)"
fi

if command -v kotlinc >/dev/null 2>&1; then
  run_one "Kotlin" "run_kotlin" "$TARGET/solution.kt"
else
  [ -f "$TARGET/solution.kt" ] && echo "(skipping Kotlin — kotlinc not found)"
fi

if command -v ruby >/dev/null 2>&1; then
  run_one "Ruby" "ruby" "$TARGET/solution.rb"
else
  [ -f "$TARGET/solution.rb" ] && echo "(skipping Ruby — ruby not found)"
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

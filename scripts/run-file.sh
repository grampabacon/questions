#!/usr/bin/env bash
#
# Run ONE solution file directly.
#
# Usage:
#   ./scripts/run-file.sh src/two-sum/solution.py
#   ./scripts/run-file.sh two-sum/solution.rb   # "src/" prefix is optional

set -uo pipefail

if [ $# -eq 0 ]; then
  echo "Usage: $0 <path-to-solution-file>"
  echo "Example: $0 src/two-sum/solution.py"
  exit 1
fi

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(dirname "$SCRIPT_DIR")"
FILE="$1"

if [ -f "$FILE" ]; then
  :
elif [ -f "$REPO_ROOT/$FILE" ]; then
  FILE="$REPO_ROOT/$FILE"
elif [ -f "$REPO_ROOT/src/$FILE" ]; then
  FILE="$REPO_ROOT/src/$FILE"
else
  echo "No such file: $1"
  exit 1
fi

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

case "$FILE" in
  *.py)   LABEL="Python";     CHECKER="python3";       CMD="python3" ;;
  *.js)   LABEL="JavaScript"; CHECKER="node";          CMD="node" ;;
  *.ts)   LABEL="TypeScript"; CHECKER="tsx";           CMD="tsx" ;;
  *.go)   LABEL="Go";         CHECKER="go";            CMD="go run" ;;
  *.cs)   LABEL="C#";         CHECKER="dotnet-script"; CMD="dotnet-script" ;;
  *.java) LABEL="Java";       CHECKER="java";          CMD="java" ;;
  *.kt)   LABEL="Kotlin";     CHECKER="kotlinc";       CMD="run_kotlin" ;;
  *.rb)   LABEL="Ruby";       CHECKER="ruby";          CMD="ruby" ;;
  *)
    echo "Unrecognized solution file extension: $FILE"
    echo "Expected one of: .py .js .ts .go .cs .java .kt .rb"
    exit 1
    ;;
esac

if ! command -v "$CHECKER" >/dev/null 2>&1; then
  echo "'$CHECKER' not found on PATH — install it to run $LABEL solutions."
  exit 1
fi

echo "Running $LABEL solution: $FILE"
echo "----------------------------------------"
if eval "$CMD \"$FILE\""; then
  echo ""
  echo "PASS"
  exit 0
else
  echo ""
  echo "FAIL"
  exit 1
fi

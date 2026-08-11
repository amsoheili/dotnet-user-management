#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT="$ROOT_DIR/User-Management.Infrastructure"
STARTUP="$ROOT_DIR/User-Management.Api"

ef() {
  dotnet ef "$@" --project "$PROJECT" --startup-project "$STARTUP"
}

usage() {
  cat <<'EOF'
Usage:
  ./ef.sh list
  ./ef.sh add <migration-name>
  ./ef.sh remove
  ./ef.sh update [migration-name]

Examples:
  ./ef.sh list
  ./ef.sh add AddBookIsbn
  ./ef.sh remove
  ./ef.sh update
  ./ef.sh update 20260722043726_AddPaidAtToPaymentInvoice
EOF
}

cmd="${1:-}"
case "$cmd" in
  list)
    ef migrations list
    ;;
  add)
    name="${2:-}"
    if [[ -z "$name" ]]; then
      echo "Error: migration name is required." >&2
      usage
      exit 1
    fi
    ef migrations add "$name"
    ;;
  remove)
    ef migrations remove
    ;;
  update)
    migration="${2:-}"
    if [[ -n "$migration" ]]; then
      ef database update "$migration"
    else
      ef database update
    fi
    ;;
  -h|--help|help|"")
    usage
    [[ -n "$cmd" ]] || exit 1
    ;;
  *)
    echo "Error: unknown command '$cmd'" >&2
    usage
    exit 1
    ;;
esac

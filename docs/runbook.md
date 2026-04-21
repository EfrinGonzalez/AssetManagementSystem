# Runbook

## Incident: Calculation jobs stuck in queued
- Check worker logs for exceptions.
- Verify hosted service startup in API logs.
- Restart container app revision.

## Incident: Slow API responses
- Inspect request logs + correlation IDs.
- Check hot path handlers and pipeline timings.

## Incident: Wrong performance values
- Validate snapshot date range and benchmark series integrity.
- Re-run job and compare attribution buckets.

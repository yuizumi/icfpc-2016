#!/bin/bash
mono --debug bin/runner.exe $1 | curl --compressed -L -H Expect: -H 'X-API-Key: 147-89469f85e8fb9eac55e53fe371218d92' -F solution_spec=@- -F problem_id=$1 http://2016sv.icfpcontest.org/api/solution/submit ; echo


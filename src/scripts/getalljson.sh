#!/bin/bash
for f in ./*.wav; do
	./getjson.sh $f > $f.json &
done
wait
sleep 1

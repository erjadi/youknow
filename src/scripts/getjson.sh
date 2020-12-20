#!/bin/bash
json=`./SpeechToWords/SpeechToWords $1`
searchstring="Json:"
rest=${json#*$searchstring}
echo $rest

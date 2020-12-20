#!/bin/bash
./util/youtube-dl https://www.youtube.com/watch?v=$youtubecode -o output.mp4 -f mp4
ffmpeg -i output.mp4 -acodec pcm_s16le -ac 1 -ar 8000 out.wav
ffmpeg -i out.wav -f segment -segment_time 10 -c copy segment%03d.wav
./getalljson.sh
./GetTimeStamps/GetTimeStamps
chmod +x script.sh
./script.sh
cp youknow.mp4 /output/youknow.mp4

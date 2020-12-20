FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build-env
WORKDIR /app
COPY ./src/GetTimeStamps/ ./GetTimeStamps/
COPY ./src/SpeechToWords/ ./SpeechToWords/
RUN dotnet publish -r linux-x64 -p:PublishSingleFile=true --self-contained true -c Release ./GetTimeStamps/
RUN dotnet publish -r linux-x64 -p:PublishSingleFile=true --self-contained true -c Release ./SpeechToWords/
RUN ls /app/SpeechToWords/bin/Release/net5.0/linux-x64/publish/

FROM mcr.microsoft.com/dotnet/runtime-deps:5.0-buster-slim
RUN apt update; exit 0
RUN apt install -y python ffmpeg ca-certificates
WORKDIR /app/GetTimeStamps
COPY --from=build-env /app/GetTimeStamps/bin/Release/net5.0/linux-x64/publish/* ./
WORKDIR /app/SpeechToWords
COPY --from=build-env /app/SpeechToWords/bin/Release/net5.0/linux-x64/publish/* ./
WORKDIR /app
COPY ./src/scripts/* /app/
RUN chmod a+x /app/*.sh
RUN mkdir /app/util
COPY ./src/util/youtube-dl /app/util
CMD ["./youknow.sh"] 

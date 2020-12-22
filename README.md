# YouKnow
YouKnow is a container image that utilizes Azure Speech Services to extract all instances of someone saying "you know" from a youtube image and stitches them all back together.

# Prerequisites
To run this project, you will need:
- An Azure Speech Services API Key (you can get one for free)
- A Linux environment with Docker installed (WSL works as well)

## Azure Speech Services
If you do not have an Azure subscription yet, you can get a [30-day free API key](https://azure.microsoft.com/en-us/try/cognitive-services/?api=speech-services).
Select **Speech services** and click on **Get API Key**. Once you have signed up, you have to take note of two things: the **api key** and the **region** that the service is deployed in.

![Image of Cognitive Service sign-up screen](/images/azure.png)

The region is the first part of the URL that is provided as the endpoint. In the above case this is **westus**.

## Run the docker image
To run this image you need to provide it with three environment variables:
- **youtubecode** - This is the part of the youtube URL that identifies the video. For example in https://www.youtube.com/watch?v=RnjxuAprpmU, youtubecode would be **RnjxuAprpmU**
- **speechregion** - This is the region that your Azure Cognitive Service is deployed in. In the above example is it **westus**
- **speechkey** - This is one of the api keys that were provided when you created the service. You can use either key.

After processing has been finished your video will be saved in the /output path inside the container.
To save the video to your local filesystem, you probably want to map this as a volume to a local directory (for example /home/eric).
We use the --volume command line parameter for this.

Putting everything together, the command line would look like this
```
docker run -e youtubecode=RnjxuAprpmU -e speechregion=westus -e speechkey=0123456789abcdef --volume=/home/eric:/output erjadi/youknow
```
Once it's finished your output video named **youknow.mp4** should be written to your mounted volume.

# How to build
To build the docker image, simply navigate to the directory with the Dockerfile and run:
```
docker build -t <yourimagename> .
```
There are two .NET 5 projects that will be built using the official Microsoft .NET 5 builder images.
In addition it will install python, ffmpeg and ca-certificates.

# Dependencies
- [FFMPEG](https://git.ffmpeg.org/ffmpeg.git) is installed into the image during docker build process.
- [Youtube-DL](https://youtube-dl.org/) binary is included in the image. 

# Known Issues
- The image is currently built for linux x64
- There is (almost) no error handling
- Audio is analyzed for speech in 10 sec cuts, if the word "you" or "know" itself is cut it most likely will not be recognized properly.

# TODO
- Code comments?
- Technical documentation 

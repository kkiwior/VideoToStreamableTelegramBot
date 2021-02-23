# Video To Streamable - Telegram Bot

## What is it?

It is simple bot written for Telegram to fetch videos from popular websites (all supported by [youtube-dl](https://ytdl-org.github.io/youtube-dl/index.html "youtube-dl")) and re-uploading them to your Streamable account. 

## Sample usage
![videotostreamable](https://raw.githubusercontent.com/kkiwior/root/master/videotostreamable.gif?token=AGVYRZIICRHBCJ2QIJBBV3DAH27RQ "videotostreamable")


## How to use?
- [Create bot](https://core.telegram.org/bots#3-how-do-i-create-a-bot "Create bot") on Telegram.
- [Create account](http://streamable.com "Create account") on Streamable.
- Pull docker image.

`docker pull xteski/videotostreamabletelegrambot:latest`
- Put your variables in command below and run docker container.
```
docker run -d \
-e StreamableAccountSettings__Login="<streamable_login_here>" \
-e StreamableAccountSettings__Password="<streamable_password_here>" \
-e TelegramBotSettings__Token="<telegram_bot_token_here>" \
xteski/videotostreamabletelegrambot
```
- It is ready now, you can send url with video that you want to re-upload in message to your bot (if you want to set custom title on Streamable just put it in message after url, in following format {url} {title}).

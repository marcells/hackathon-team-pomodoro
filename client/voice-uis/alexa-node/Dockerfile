# Temporary build image
FROM node:9.5.0-slim AS builder

ADD service /team-pomodoro-voice-alexa

WORKDIR /team-pomodoro-voice-alexa
RUN npm install --silent

# Production
FROM node:9.5.0-slim

COPY --from=builder /team-pomodoro-voice-alexa /team-pomodoro-voice-alexa
EXPOSE 3000
CMD [ "node", "/team-pomodoro-voice-alexa/app.js"]%

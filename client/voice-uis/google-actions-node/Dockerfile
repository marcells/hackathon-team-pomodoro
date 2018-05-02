# Temporary build image
FROM node:9.5.0-slim AS builder

ADD service /team-pomodoro-voice-google

WORKDIR /team-pomodoro-voice-google
RUN npm install --silent

# Production
FROM node:9.5.0-slim

COPY --from=builder /team-pomodoro-voice-google /team-pomodoro-voice-google
EXPOSE 3000
CMD [ "node", "/team-pomodoro-voice-google/app.js"]%

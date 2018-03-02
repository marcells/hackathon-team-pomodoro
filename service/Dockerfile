# Temporary build image
FROM node:9.5.0-slim AS builder

ADD app /team-pomodoro

WORKDIR /team-pomodoro
RUN npm install --silent

# Production
FROM node:9.5.0-slim

COPY --from=builder /team-pomodoro /team-pomodoro
EXPOSE 3000
CMD [ "node", "/team-pomodoro/app.js"]%      
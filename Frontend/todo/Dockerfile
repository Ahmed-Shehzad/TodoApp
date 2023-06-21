# Use the official Node.js 14 image as the base image
FROM node:lts-alpine3.18

# Set the working directory inside the container
WORKDIR /app

# Copy package.json and package-lock.json to the working directory
COPY package*.json .

# Install dependencies
RUN npm install

# Copy the rest of the application code to the working directory
COPY . .

# Expose the Next.js default port (usually 3000)
EXPOSE 3000

# Build the Next.js application
RUN npm run build

# Start the Next.js application
CMD ["npm", "start"]

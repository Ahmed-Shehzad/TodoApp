/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
  env: {
    apiUrl: "https://localhost:5001/api",
    // apiUrl: "https://localhost:7113/api",
  },
};

module.exports = nextConfig;

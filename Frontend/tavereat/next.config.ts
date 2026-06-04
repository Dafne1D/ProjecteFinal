import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  images: {
    remotePatterns: [      
      {
        protocol: "http",
        hostname: "localhost",
        pathname: "/**",
      },
      {
        protocol: "https",
        hostname: "encrypted-tbn0.gstatic.com",
        pathname: "/**",
      },
    ],
  },
};

module.exports = nextConfig;
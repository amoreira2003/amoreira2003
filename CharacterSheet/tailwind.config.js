/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        'fake-white': '#FAF9F6'
      },
      fontFamily: {
        'varela': ['Varela Round']
      },
      animation: {
        'spin-slow': 'spin 2s linear infinite'
      }
    },
  },
  plugins: [],
}
# Cinema-Booking-System

A comprehensive cinema ticket booking application that enables users to browse movies, select showtime, choose seats, and complete bookings seamlessly.

## Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [API Endpoints](#api-endpoints)
- [Database Schema](#database-schema)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Movie Listings**: Browse available movies with descriptions, ratings, and genres
- **Showtime Management**: View multiple showtimes for each movie across different dates
- **Seat Selection**: Interactive seat map with real-time availability updates
- **Booking System**: Secure ticket booking with confirmation
- **User Authentication**: Secure login and registration
- **Booking History**: View past and upcoming bookings
- **Payment Integration**: Support for multiple payment methods
- **Admin Dashboard**: Manage movies, showtimes, and bookings (admin only)
- **Responsive Design**: Works seamlessly on desktop and mobile devices

## Tech Stack

- **Frontend**: [Your Frontend Framework - e.g., React, Vue, Angular]
- **Backend**: [Your Backend Language - e.g., Node.js/Express, Python/Django, Java/Spring]
- **Database**: [Your Database - e.g., MongoDB, PostgreSQL, MySQL]
- **Authentication**: JWT / OAuth
- **Payment Gateway**: [e.g., Stripe, PayPal]
- **Hosting**: [Your hosting platform]

## Installation

### Prerequisites

- Node.js v14+ or Python 3.8+ (depending on your stack)
- npm or pip package manager
- Database setup (MongoDB/PostgreSQL/MySQL)

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/nourMelegy/Cinema-Booking-System.git
   cd Cinema-Booking-System
   ```

2. **Install dependencies**
   ```bash
   # For Node.js/Frontend
   npm install
   
   # For Python backend
   pip install -r requirements.txt
   ```

3. **Configure environment variables**
   ```bash
   cp .env.example .env
   # Edit .env with your configuration
   ```

4. **Setup database**
   ```bash
   # Run migrations or setup scripts
   npm run migrate
   # or
   python manage.py migrate
   ```

5. **Start the application**
   ```bash
   # Development mode
   npm run dev
   # or
   python manage.py runserver
   ```

## Usage

### For Users

1. **Register/Login**: Create an account or log in with existing credentials
2. **Browse Movies**: Explore available movies with details
3. **Select Showtime**: Choose your preferred date and time
4. **Choose Seats**: Select available seats from the interactive map
5. **Complete Payment**: Proceed to checkout and complete payment
6. **Confirmation**: Receive booking confirmation via email

### For Administrators

1. Access the admin dashboard with admin credentials
2. Manage movie listings (add, edit, delete)
3. Update showtimes and pricing
4. View and manage all bookings
5. Generate reports and analytics

## Project Structure

```
Cinema-Booking-System/
├── frontend/                 # Frontend application
│   ├── src/
│   │   ├── components/       # Reusable React components
│   │   ├── pages/            # Page components
│   │   ├── services/         # API services
│   │   └── assets/           # Images and styles
│   └── package.json
├── backend/                  # Backend application
│   ├── app/
│   │   ├── models/           # Database models
│   │   ├── routes/           # API routes
│   │   ├── controllers/      # Business logic
│   │   └── middleware/       # Authentication & validation
│   ├── config/               # Configuration files
│   └── requirements.txt
├── database/
│   └── schema.sql            # Database schema
├── docs/                     # Documentation
├── .env.example              # Environment variables template
└── README.md
```

## API Endpoints

### Authentication
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login
- `POST /api/auth/logout` - User logout
- `POST /api/auth/refresh` - Refresh token

### Movies
- `GET /api/movies` - Get all movies
- `GET /api/movies/:id` - Get movie details
- `POST /api/movies` - Create movie (admin only)
- `PUT /api/movies/:id` - Update movie (admin only)
- `DELETE /api/movies/:id` - Delete movie (admin only)

### Showtimes
- `GET /api/showtimes` - Get all showtimes
- `GET /api/showtimes?movieId=:id` - Get showtimes by movie
- `POST /api/showtimes` - Create showtime (admin only)
- `PUT /api/showtimes/:id` - Update showtime (admin only)

### Bookings
- `GET /api/bookings` - Get user's bookings
- `POST /api/bookings` - Create new booking
- `GET /api/bookings/:id` - Get booking details
- `DELETE /api/bookings/:id` - Cancel booking

### Seats
- `GET /api/seats/showtime/:id` - Get available seats for a showtime

## Database Schema

Key tables:
- **users**: User account information
- **movies**: Movie details (title, description, genre, duration, rating)
- **showtimes**: Movie showtimes with theater, date, time, and pricing
- **bookings**: User bookings with status and payment information
- **seats**: Cinema seats with availability status
- **payments**: Payment transaction records

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

Please ensure your code follows the project's coding standards and includes appropriate tests.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For support, please open an issue on the GitHub repository or contact [your-email@example.com].

## Acknowledgments

- Thanks to all contributors who have helped with this project
- Inspired by modern cinema booking platforms

---

**Note**: This is a template README. Please update the sections marked with brackets to match your specific implementation, technology stack, and project details.

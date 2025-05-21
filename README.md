# BookStore Application User Guide

## Overview
BookStore is a desktop application for managing and reading books, with different features based on user roles.

## Installation

1. Download the latest release from the repository
2. Extract all files to your preferred location
3. Run `BookStoreApp.exe` to start the application

## User Roles

### Guest (Not Logged In)
- Login to existing account
- Register new account

### User
- Browse available books in the Book List
- Read owned books
- Submit book requests
- Manage account settings
- Change application language

### Manager
- Manage authors
- Manage genres
- Manage books
- View and process book orders
- View book requests
- Manage account settings

### Admin
- Access system dashboard (User management)
- Manage account settings

## Feature Guide

### Authentication
- Use the login screen to enter credentials

![login.png](BookStoreApp/readme-files/login.png)

- New users can register via the Registration page

![registration.png](BookStoreApp/readme-files/registration.png)

- Logout via the navigation menu

![logout.png](BookStoreApp/readme-files/logout.png)

### Book List (User)
- View all available books for purchase
- Filter and search for books
- Purchase books to add to your collection

![bookshop.png](BookStoreApp/readme-files/bookshop.png)

### Owned Books (User)
- View books you've purchased
- Open a book to view details or read content
- Books display cover, author, genres, and description

![owned-books.png](BookStoreApp/readme-files/owned-books.png)

### Reading Books (User)
1. Select a book from your owned books

![read-book.png](BookStoreApp/readme-files/read-book.png)

2. Click "Read" to flip to the reading view
3. Scroll through the book content

![book-content.png](BookStoreApp/readme-files/book-content.png)

4. Click "Back" to return to book details

### Book Requests (User)
1. Navigate to "Add Book Request"
2. Fill in book details (title, author, publication year)
3. Submit the request
4. A confirmation toast will appear when submitted

![book-request.png](BookStoreApp/readme-files/book-request.png)

### Managing Authors/Genres/Books (Manager)
- Add, edit, or remove authors

![authors.png](BookStoreApp/readme-files/authors.png)

- Create and manage book genres

![genres.png](BookStoreApp/readme-files/genres.png)

- Add new books with details and content

![books.png](BookStoreApp/readme-files/books.png)

### Book Orders (Manager)
- View all book orders sorted by date
- Filter orders by status (pending, completed, etc.)
- View order details including user information and book details
- Mark orders as completed or pending

![book-orders.png](BookStoreApp/readme-files/book-orders.png)

### Book Requests (Manager)
- View all book requests sorted by date

![book-requests-list.png](BookStoreApp/readme-files/book-requests-list.png)

### User Management (Admin)
- View all users in the system
- Edit user details
- Delete users if necessary
- Create new users

![user-management.png](BookStoreApp/readme-files/user-management.png)

### Settings (User/Manager/Admin)
- Change application theme (dark/light) and primary color

![settings.png](BookStoreApp/readme-files/settings.png)

### Language Support (User/Manager/Admin)
- Available languages can be changed from the dropdown in the top right
- The app will immediately update all text to the selected language

![language.png](BookStoreApp/readme-files/language.png)

## Troubleshooting
- If you encounter database errors, ensure the application has the necessary permissions
- For visual issues, try restarting the application

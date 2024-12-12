using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Blazor_Lab_Starter_Code;

namespace Blazor_Lab_Starter_Code.Tests {
    [TestFixture]
    public class LibraryServiceTests {

        public List<Book> books;
        public List<User> users;
        public Dictionary<User, List<Book>> borrowedBooks;

        [SetUp]
        public void Setup() {
            books = new List<Book>();
            users = new List<User>();
            borrowedBooks = new Dictionary<User, List<Book>>();
        }

        [Test]
        public void AddBook_ShouldAddBookToList() {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book", Author = "Test Author", ISBN = "12345" };

            // Act
            books.Add(book);

            // Assert
            Assert.That(books, Has.Count.EqualTo(1));
            Assert.That(books.First(), Is.EqualTo(book));
        }

        [Test]
        public void EditBook_ShouldUpdateBookDetails() {
            // Arrange
            var book = new Book { Id = 1, Title = "Old Title", Author = "Old Author", ISBN = "12345" };
            books.Add(book);

            // Act
            var bookToEdit = books.First(b => b.Id == 1);
            bookToEdit.Title = "New Title";
            bookToEdit.Author = "New Author";

            // Assert
            Assert.That(bookToEdit.Title, Is.EqualTo("New Title"));
            Assert.That(bookToEdit.Author, Is.EqualTo("New Author"));
        }

        [Test]
        public void DeleteBook_ShouldRemoveBookFromList() {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book", Author = "Test Author", ISBN = "12345" };
            books.Add(book);

            // Act
            books.Remove(book);

            // Assert
            Assert.That(books, Is.Empty);
        }

        [Test]
        public void AddUser_ShouldAddUserToList() {
            // Arrange
            var user = new User { Id = 1, Name = "Test User", Email = "test@example.com" };

            // Act
            users.Add(user);

            // Assert
            Assert.That(users, Has.Count.EqualTo(1));
            Assert.That(users.First(), Is.EqualTo(user));
        }

        [Test]
        public void EditUser_ShouldUpdateUserDetails() {
            // Arrange
            var user = new User { Id = 1, Name = "Old Name", Email = "old@example.com" };
            users.Add(user);

            // Act
            var userToEdit = users.First(u => u.Id == 1);
            userToEdit.Name = "New Name";
            userToEdit.Email = "new@example.com";

            // Assert
            Assert.That(userToEdit.Name, Is.EqualTo("New Name"));
            Assert.That(userToEdit.Email, Is.EqualTo("new@example.com"));
        }

        [Test]
        public void DeleteUser_ShouldRemoveUserFromList() {
            // Arrange
            var user = new User { Id = 1, Name = "Test User", Email = "test@example.com" };
            users.Add(user);

            // Act
            users.Remove(user);

            // Assert
            Assert.That(users, Is.Empty);
        }

        [Test]
        public void BorrowBook_ShouldReduceBookCount() {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book", Author = "Test Author", ISBN = "12345" };
            books.Add(book);
            var user = new User { Id = 1, Name = "Test User", Email = "test@example.com" };

            // Act
            if (!borrowedBooks.ContainsKey(user)) {
                borrowedBooks[user] = new List<Book>();
            }
            borrowedBooks[user].Add(book);
            books.Remove(book);

            // Assert
            Assert.That(books, Is.Empty);
            Assert.That(borrowedBooks[user], Has.Count.EqualTo(1));
            Assert.That(borrowedBooks[user].First(), Is.EqualTo(book));
        }

        [Test]
        public void ReturnBook_ShouldIncreaseBookCount() {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book", Author = "Test Author", ISBN = "12345" };
            var user = new User { Id = 1, Name = "Test User", Email = "test@example.com" };
            borrowedBooks[user] = new List<Book> { book };

            // Act
            borrowedBooks[user].Remove(book);
            books.Add(book);

            // Assert
            Assert.That(books, Has.Count.EqualTo(1));
            Assert.That(borrowedBooks[user], Is.Empty);
        }
    }
}
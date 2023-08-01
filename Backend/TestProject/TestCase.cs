using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using TransactionProcessingAPI.Controllers;
using TransactionProcessingAPI.Services;
using TransactionProcessingAPI.Models;
using Xunit;
using Transaction = TransactionProcessingAPI.Models.Transaction;
using System.Net;

namespace TransactionAPI.Tests
{
	public class TransactionControllerTests
	{
		private TransactionController _controller;
		private Mock<ITransactionService> _transactionServiceMock;

		public TransactionControllerTests()
		{
			_transactionServiceMock = new Mock<ITransactionService>();
			_controller = new TransactionController(_transactionServiceMock.Object);
		}

		[Fact]
		public async Task GetTransactions_ReturnsOkResultWithTransactions()
		{
			// Arrange
			var transactions = new List<Transaction> { new Transaction(), new Transaction() };
			_transactionServiceMock.Setup(service => service.GetTransactions()).ReturnsAsync(transactions);

			// Act
			var result = await _controller.GetTransactions();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			Assert.Equal(transactions, okResult.Value);
		}
		[Fact]
		public async Task CreateTransaction_InvalidTransaction_ReturnsBadRequest()
		{
			// Arrange
			var invalidTransaction = new Transaction { Amount = -50, SenderAccountNumber = "John", ReceiverAccountNumber = "Jane" };

			_controller.ModelState.AddModelError("Amount", "Invalid amount");

			// Act
			var result = await _controller.CreateTransaction(invalidTransaction);

			// Assert
			Assert.IsType<BadRequestObjectResult>(result.Result);
		}

		[Fact]
		public async Task GetTransaction_ExistingId_ReturnsOkResultWithTransaction()
		{
			// Arrange
			var transactionId = 1;
			var transaction = new Transaction { Id = transactionId };
			_transactionServiceMock.Setup(service => service.GetTransaction(transactionId)).ReturnsAsync(transaction);

			// Act
			var result = await _controller.GetTransaction(transactionId);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			Assert.Equal(transaction, okResult.Value);
		}

		[Fact]
		public async Task GetTransaction_NonExistingId_ReturnsNotFoundResult()
		{
			// Arrange
			var transactionId = 1;
			_transactionServiceMock.Setup(service => service.GetTransaction(transactionId)).ReturnsAsync((Transaction)null);

			// Act
			var result = await _controller.GetTransaction(transactionId);

			// Assert
			Assert.IsType<NotFoundResult>(result.Result);
		}

		// Add more test cases for other actions such as CreateTransaction, UpdateTransaction, DeleteTransaction, etc.
		// You can mock the necessary dependencies and verify the expected behavior of the controller.


	}
}

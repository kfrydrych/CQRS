using CQRS.Validation;
using NUnit.Framework;
using System.Linq;

namespace CQRS.Tests.Validation
{
    [TestFixture]
    public class ActionPossibleTests
    {
        [Test]
        public void ActionPossible_WhenTrue_ReturnsCorrectInfo()
        {
            var result = ActionPossible.True();

            Assert.Multiple(() =>
            {
                Assert.That(result.IsPossible, Is.True);
                Assert.That(result.IsImpossible, Is.False);
                Assert.That(result.Errors, Is.Empty);
            });
        }

        [Test]
        public void ActionPossible_WhenUnauthorized_ReturnsCorrectInfo()
        {
            var result = ActionPossible.Unauthorized();

            Assert.Multiple(() =>
            {
                Assert.That(result.IsPossible, Is.False);
                Assert.That(result.IsImpossible, Is.True);
                Assert.That(result.Errors, Contains.Item("Unauthorized Action"));
            });
        }

        [Test]
        public void ActionPossible_WhenFalseWithSingleError_ReturnsCorrectInfo()
        {
            var result = ActionPossible.False("You cannot perform this action");

            Assert.Multiple(() =>
            {
                Assert.That(result.IsPossible, Is.False);
                Assert.That(result.IsImpossible, Is.True);
                Assert.That(result.Errors, Contains.Item("You cannot perform this action"));
            });
        }

        [Test]
        public void ActionPossible_WhenFalseWithManyError_ReturnsCorrectInfo()
        {
            ActionPossible.AddError("Error message 1");
            ActionPossible.AddError("Error message 2");
            ActionPossible.AddError("Error message 3");

            var result = ActionPossible.FalseWithErrors();

            Assert.Multiple(() =>
            {
                Assert.That(result.IsPossible, Is.False);
                Assert.That(result.IsImpossible, Is.True);
                Assert.That(result.Errors.Count(), Is.EqualTo(3));
                Assert.That(result.Errors, Contains.Item("Error message 1"));
                Assert.That(result.Errors, Contains.Item("Error message 2"));
                Assert.That(result.Errors, Contains.Item("Error message 3"));
            });
        }

        [Test]
        public void ActionPossible_WhenReturned_StaticErrorListBecomesEmpty_DefaultMessageInErrorList()
        {
            ActionPossible.AddError("Error message 1");
            ActionPossible.AddError("Error message 2");
            ActionPossible.AddError("Error message 3");

            var result = ActionPossible.FalseWithErrors();

            Assert.That(result.Errors.Count(), Is.EqualTo(3));

            var nextResult = ActionPossible.FalseWithErrors();

            Assert.Multiple(() =>
            {
                Assert.That(nextResult.Errors.Count(), Is.EqualTo(1));
                Assert.That(nextResult.Errors, Contains.Item("You are not allowed to perform this action"));
            });
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace CQRS.Validation
{
    public class ActionPossible
    {
        private static readonly IList<string> _temporaryList = new List<string>();
        public static void AddError(string messsage)
        {
            _temporaryList.Add(messsage);
        }


        private readonly IList<string> _errorList = new List<string>();

        private ActionPossible(bool isPossible, bool isImpossible)
        {
            IsPossible = isPossible;
            IsImpossible = isImpossible;
            _temporaryList.Clear();
        }

        private ActionPossible(bool isPossible, bool isImpossible, string message)
        {
            IsPossible = isPossible;
            IsImpossible = isImpossible;
            _errorList.Add(message);
            _temporaryList.Clear();
        }

        private ActionPossible(bool isPossible, bool isImpossible, IList<string> errors)
        {
            if (!errors.Any())
                errors.Add("You are not allowed to perform this action");

            IsPossible = isPossible;
            IsImpossible = isImpossible;
            _errorList = errors.ToList();
            _temporaryList.Clear();
        }


        public IEnumerable<string> Errors => _errorList;
        public bool IsPossible { get; }
        public bool IsImpossible { get; }


        public static ActionPossible True()
        {
            return new ActionPossible(true, false);
        }

        public static ActionPossible False(string message)
        {
            return new ActionPossible(false, true, message);
        }

        public static ActionPossible FalseWithErrors()
        {
            return new ActionPossible(false, true, _temporaryList);
        }

        public static ActionPossible Unauthorized()
        {
            return new ActionPossible(false, true, "Unauthorized Action");
        }
    }
}

namespace Problem04.BalancedParentheses
{
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            if (string.IsNullOrEmpty(parentheses) || parentheses.Length % 2 != 0)
            {
                return false;
            }

            var stack = new Stack<char>();

            foreach (var parenthes in parentheses)
            {
                char expectedParentheses = default;
                var isClosed = false;

                if (parenthes == '(' || parenthes == '[' || parenthes == '{')
                {
                    stack.Push(parenthes);
                    continue;
                }
                else if (parenthes == ')')
                {
                    expectedParentheses = '(';
                    isClosed = true;
                }
                else if (parenthes == ']')
                {
                    expectedParentheses = '[';
                    isClosed = true;
                }
                else if (parenthes == '}')
                {
                    expectedParentheses = '{';
                    isClosed = true;
                }

                if (isClosed)
                {
                    if (stack.Count == 0 || stack.Pop() != expectedParentheses)
                    {
                        return false;
                    }
                }
            }

            if (stack.Count == 0)
            {
                return true;
            }

            return false;
        }
    }
}

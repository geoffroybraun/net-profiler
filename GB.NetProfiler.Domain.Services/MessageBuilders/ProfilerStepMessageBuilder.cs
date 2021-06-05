using GB.NetProfiler.Domain.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("GB.NetProfiler.Application.Services")]
namespace GB.NetProfiler.Domain.Services.MessageBuilders
{
    /// <summary>
    /// Builds a recursive message from the provided <see cref="ProfilerStepComposite"/> entity
    /// </summary>
    internal static class ProfilerStepMessageBuilder
    {
        #region Fields

        private const string CompositeLayout = "{0}[{1}] {2} | {3}ms\r\n";
        private const string LeafLayout = "{0}[{1}] {2}\r\n";

        #endregion

        /// <summary>
        /// Builds a recursive message for all provided <see cref="BaseProfilerStep"/> entities
        /// </summary>
        /// <param name="rootSteps">The <see cref="BaseProfilerStep"/> entities to use when building the message</param>
        /// <returns>The build message</returns>
        public static string Build(IEnumerable<BaseProfilerStep> rootSteps)
        {
            var result = new StringBuilder();

            foreach (var rootStep in rootSteps)
                result.Append(Build(rootStep, 0));

            return result.ToString();
        }

        #region Private methods

        private static string Build(BaseProfilerStep step, int offset)
        {
            var result = new StringBuilder();

            if (step is ProfilerStepComposite composite)
            {
                result.Append(Build(composite, offset));

                return result.ToString();
            }

            result.Append(Build(step as ProfilerStepLeaf, offset));

            return result.ToString();
        }

        private static string Build(ProfilerStepComposite composite, int offset)
        {
            var result = new StringBuilder();
            result.Append(string.Format(CompositeLayout, GetOffset(offset), composite.Type.ToString(), composite.Message, composite.ExecutionTime.TotalMilliseconds));

            if (composite.Children.Any())
                foreach (var child in composite.Children)
                {
                    result.Append(Build(child, ++offset));
                    offset--;
                }

            return result.ToString();
        }

        private static string Build(ProfilerStepLeaf leaf, int offset)
        {
            var offsetAsString = GetOffset(offset);

            return string.Format(LeafLayout, offsetAsString, leaf.Type.ToString(), leaf.Message);
        }

        private static string GetOffset(int offset)
        {
            var result = new StringBuilder();

            for (int i = 0; i < offset; i++)
                result.Append('\t');

            return result.ToString();
        }

        #endregion
    }
}

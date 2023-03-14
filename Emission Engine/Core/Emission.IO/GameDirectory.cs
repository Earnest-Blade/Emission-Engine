using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Enumeration;
using System.Text;
using JetBrains.Annotations;

namespace Emission.IO
{
    public static class GameDirectory
    {
        /// <summary>
        /// Define current working directory.
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void SetCurrentDirectory([CanBeNull] string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (path.Length == 0) throw new ArgumentException();
            
            Environment.CurrentDirectory = Path.GetFullPath(path);
        }

        public static string GetDirectoryFromFilePath(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (path.Length == 0) throw new ArgumentException();
            
            int end = GetDirectoryNameOffset(path.AsSpan());
            return end >= 0 ? NormalizeDirectorySeparators(path.Substring(0, end) + "/") : String.Empty;
        }
        
        public static ReadOnlySpan<char> GetDirectoryFromFilePath(ReadOnlySpan<char> path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (path.Length == 0) throw new ArgumentException();

            int end = GetDirectoryNameOffset(path);
            return end >= 0 ? path.Slice(0, end) : ReadOnlySpan<char>.Empty;
        }
        
        /// <summary>
        /// Return current working directory.
        /// </summary>
        public static string GetCurrentDirectory() => Environment.CurrentDirectory;

        public static IEnumerable<string> EnumerateFiles(string path) => EnumeratePaths(path, "*", SearchTarget.Files,
            new EnumerationOptions { MatchType = MatchType.Win32, AttributesToSkip = 0, IgnoreInaccessible = false });

        internal static IEnumerable<string> EnumeratePaths(string path, string searchPattern, SearchTarget searchTarget,
            EnumerationOptions options)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (searchPattern == null)
                throw new ArgumentNullException(nameof(searchPattern));

            switch (searchTarget)
            {
                case SearchTarget.Files:
                    return new FileSystemEnumerable<string>(path,
                        (ref FileSystemEntry entry) => entry.ToSpecifiedFullPath(), options)
                    {
                        ShouldIncludePredicate = (ref FileSystemEntry entry) =>
                            !entry.IsDirectory && MatchesPattern(searchPattern, entry.FileName, options)
                    };
                case SearchTarget.Directories:
                    return new FileSystemEnumerable<string>(path,
                        (ref FileSystemEntry entry) => entry.ToSpecifiedFullPath(), options)
                    {
                        ShouldIncludePredicate = (ref FileSystemEntry entry) =>
                            entry.IsDirectory && MatchesPattern(searchPattern, entry.FileName, options)
                    };
                case SearchTarget.Both:
                    return new FileSystemEnumerable<string>(path,
                        (ref FileSystemEntry entry) => entry.ToSpecifiedFullPath(), options)
                    {
                        ShouldIncludePredicate = (ref FileSystemEntry entry) =>
                            MatchesPattern(searchPattern, entry.FileName, options)
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(searchTarget));
            }
        }

        /// <summary>
        /// Returns the directory portion of a file path. The returned value is empty
        /// if the specified path is null, empty, or a root.
        /// </summary>
        private static int GetDirectoryNameOffset(ReadOnlySpan<char> path)
        {
            int rootLength = Path.GetPathRoot(path).Length;
            int end = path.Length;
            if (end <= rootLength) 
                return -1;

            // End will be equal to the first Directory Char Separator from the end. 
            while (end > rootLength && !IsDirectorySeparator(path[--end])) { }

            // Trim off any remaining separators
            while (end > rootLength && IsDirectorySeparator(path[end - 1])) end--;
            
            return end;
        }
        
        private static bool MatchesPattern(string expression, ReadOnlySpan<char> name, EnumerationOptions options)
        {
            bool ignoreCase = (options.MatchCasing == MatchCasing.PlatformDefault) || options.MatchCasing == MatchCasing.CaseInsensitive;

            return options.MatchType switch
            {
                MatchType.Simple => FileSystemName.MatchesSimpleExpression(expression.AsSpan(), name, ignoreCase),
                MatchType.Win32 => FileSystemName.MatchesWin32Expression(expression.AsSpan(), name, ignoreCase),
                _ => throw new ArgumentOutOfRangeException(nameof(options)),
            };
        }

        /// <summary>
        /// Return true if the given character is a directory separator.
        /// </summary>
        internal static bool IsDirectorySeparator(char c) 
            => c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
        
        [return: NotNullIfNotNull("path")] [CanBeNull]
        internal static string NormalizeDirectorySeparators([CanBeNull] string path)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            char current;
            bool normalized = true;

            for (int i = 0; i < path.Length; i++)
            {
                current = path[i];
                if (!IsDirectorySeparator(current) || (current == Path.DirectorySeparatorChar &&
                                                       (i <= 0 || i + 1 >= path.Length ||
                                                        !IsDirectorySeparator(path[i + 1])))) continue;
                normalized = false;
                break;
            }

            if (normalized)
                return path;

            StringBuilder builder = new StringBuilder();

            int start = 0;
            if (IsDirectorySeparator(path[start]))
            {
                start++;
                builder.Append(Path.DirectorySeparatorChar);
            }

            for (int i = start; i < path.Length; i++)
            {
                current = path[i];

                // If we have a separator
                if (IsDirectorySeparator(current))
                {
                    // If the next is a separator, skip adding this
                    if (i + 1 < path.Length && IsDirectorySeparator(path[i + 1])) continue;

                    current = Path.DirectorySeparatorChar;
                }

                builder.Append(current);
            }

            return builder.ToString();
        }
        
        internal enum SearchTarget : byte
        {
            Files = 0x1,
            Directories = 0x2,
            Both = 0x3
        }
    }
}

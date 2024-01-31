﻿namespace Verify.Nupkg;

public static class VerifySettingsExtensions
{
    private static readonly NuspecScrubber _scrubber = new();

    /// <summary>
    /// Scrub the nuspec file of common changes.
    /// </summary>
    /// <param name="settings">
    /// The <see cref="VerifySettings"/> to modify.
    /// </param>
    /// <remarks>
    /// Sources of common changes:
    /// - Package version
    /// - Commit hash
    /// </remarks>
    public static void ScrubNuspec(this VerifySettings settings)
    {
        settings.ScrubNuspecVersion();
        settings.ScrubNuspecCommit();
    }

    /// <summary>
    /// Scrub the version from the nuspec file.
    /// </summary>
    /// <param name="settings">
    /// The <see cref="VerifierSettings"/> to modify.
    /// </param>
    /// <remarks>
    /// In some scenarios, package versions are frequent and obvious changes. In these cases, scrub the version
    /// to reduce the noise in the diff.
    /// </remarks>
    public static void ScrubNuspecVersion(this VerifySettings settings)
    {
        settings.ScrubLinesWithReplace(extension: "nuspec", line => _scrubber.ScrubVersion(line));
    }

    /// <summary>
    /// Scrub the commit info from the nuspec file.
    /// </summary>
    /// <param name="settings">
    /// The <see cref="VerifierSettings"/> to modify.
    /// </param>
    /// <remarks>
    /// In some scenarios, the commit used in the package are frequent and obvious changes. In these cases, scrub the commit
    /// to reduce the noise in the diff.
    /// </remarks>
    public static void ScrubNuspecCommit(this VerifySettings settings)
    {
        settings.ScrubLinesWithReplace(extension: "nuspec", line => _scrubber.ScrubCommit(line));
    }
}

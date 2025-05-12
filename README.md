📝 Project Overview & Test Documentation

This project contains automated tests built for the Tokero platform, using Playwright for browser automation and NUnit as the test runner and framework. The goal is to validate core functionality, catch regressions, and ensure platform stability across different environments and configurations.

🔍 Main Features of the Testing Suite
🌐 Cross-Browser Support

All UI tests are designed to automatically run in three major browsers:

    Chromium

    Firefox

    WebKit

This is enabled via a custom [MultiBrowserTest] attribute, which dynamically generates test cases for each browser. This ensures a higher level of confidence that the application behaves consistently across engines.
🔄 Exchange Page Testing

The Exchange page is a central feature of the Tokero platform. The following tests have been implemented:

    ✅ Ensures that the default currency parity selected on load is EUR.

    ✅ Allows switching between different parities (e.g., RON, USD) and verifies that:

        The selected parity updates correctly in the UI.

        The data table showing available coins is populated as expected.

🌍 Language Switching Validation

Tokero supports multiple languages. A test has been added to:

    ✅ Select a language (e.g., EN, RO, HU) via the language switcher.

    ✅ Assert that a known keyword for that language appears on the page, confirming the translation is applied.

🔌 API Testing

Tests are also written for backend API validation using standard HttpClient calls:

    ✅ A performance test ensures that the API responds within 2 seconds, guarding against backend slowness or timeout risks.

🧰 Test Artifacts and Reporting

    Test output is stored in the TestResults/ folder.

    Playwright trace files (ZIP archives) are generated for failed tests to allow debugging via Playwright Trace Viewer.

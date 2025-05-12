📝 Project Overview & Test Documentation

This project contains automated tests built for the Tokero platform, using Playwright for browser automation and NUnit as the test framework. The goal is to validate core functionality and ensure platform stability across different environments and configurations.

All UI tests are designed to automatically run in three browsers:

    Chromium

    Firefox

    WebKit
This is enabled via a custom [MultiBrowserTest] attribute, which dynamically generates test cases per browser — helping ensure consistency across rendering engines.


Exchange Page Testing

The following aspects are tested:

    The default parity selected on load is EUR.

    Switching to another parity (e.g., RON, USD) updates:

        The selected parity shown in the UI.

        The data table content, confirming coin list updates correctly.


Language Switching

To ensure multilingual support works as expected:

     Select a language (e.g., EN, RO, HU) via the language switcher.

     Verify a specific keyword appears in that language, confirming the translation is applied.
     
     Note: At the time of testing, the Polish language option was not functioning correctly, resulting in a 404 error and causing 3 tests to fail. See the trace files for further details.

API Testing

API-level tests ensure backend services are responsive and functional:

    The coin-pair price endpoint responds with status 200 OK and valid data.

    A performance test ensures the response time is under 2 seconds — helping flag backend latency issues.

Test Artifacts & Reporting

    Test result files are saved in the TestResults/ directory.

    Playwright trace files (.zip) are generated on failure and stored under TestResults/playwright-traces/.

    These can be opened with Playwright Trace Viewer for visual debugging.

    Test results are also exported to .trx.

Tradeoffs and Architectural Notes

To prioritize speed and functional coverage, some compromises were made:

    Limited abstraction

    Hardcoded selectors: Used in certain tests for speed instead of fully encapsulating everything in page objects.

    Minimal documentation in code: While readable, comments & documentation could be improved.


import { chromium } from 'playwright';

const browser = await chromium.launch();
try {
  const page = await browser.newPage();
  page.setDefaultTimeout(60000);
  const errors = [];
  page.on('pageerror', error => errors.push(error.message));
  page.on('console', message => {
    if (message.type() === 'error') errors.push(message.text());
  });
  await page.goto('https://anugtools.azurewebsites.net/', { waitUntil: 'networkidle' });
  await page.getByRole('heading', { name: "Let's Find All the Winners" }).waitFor();
  const events = page.getByLabel('Select Meetup Event', { exact: true });
  await events.waitFor();
  // Blazor renders before its initial API request completes. Clicking waits
  // for the selector to become enabled instead of sampling that transient state.
  await events.click();
  await page.locator('.mud-popover-open .mud-list-item').first().waitFor();
  if (await page.locator('#blazor-error-ui').isVisible()) throw new Error('Blazor error banner is visible');
  if (errors.length) throw new Error(errors.join('\n'));
  await page.screenshot({ path: 'live-site.png', fullPage: true });
  console.log('Live Blazor startup and event loading passed.');
} finally {
  await browser.close();
}

export async function registerAccount(
  sessionToken,
  userId,
  accountNumber,
  sortCode,
  balance
) {
  const baseUrl = 'https://localhost:44356/Account/RegisterAccount';
  const qStrings = `?sessionToken=${sessionToken}&userId=${userId}&accountNumber=${accountNumber}&sortCode=${sortCode}&balance=${balance}`;

  const response = await fetch(baseUrl + qStrings, {
    method: 'PUT',
    mode: 'cors',
    cache: 'no-cache',
  }).then((res) => res.json());
  return response;
}
export async function createWithdrawal(
  accountId,
  sessionToken,
  accountNumber,
  amount
) {
  const baseUrl = 'https://localhost:44356/Withdraw/Create';

  console.log('accountId', accountId);
  console.log('sessionToken', sessionToken);
  console.log('amount', amount);
  const response = await fetch(baseUrl, {
    method: 'POST',
    mode: 'cors',
    cache: 'no-cache',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      AccountId: accountId,
      SessionToken: sessionToken,
      Amount: parseInt(amount),
    }),
  }).then((res) => res.json());
  return response;
}

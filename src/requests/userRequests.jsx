export async function registerUser(
  userName,
  password,
  firstName,
  lastName,
  address,
  postcode,
  mobile,
  email
) {
  const baseUrl = 'https://localhost:44356/User/Register?';
  const qStrings = `userName=${userName}&password=${password}&firstName=${firstName}&lastName=${lastName}&address=${address}&postcode=${postcode}&mobile=${mobile}&email=${email}`;

  const response = await fetch(baseUrl + qStrings, {
    method: 'PUT',
    mode: 'cors',
    cache: 'no-cache',
  }).then((res) => res.status === 200);
  return response;
}
export async function authenticateUser(userName, password) {
  const baseUrl = 'https://localhost:44356/User/Authenticate';
  const response = await fetch(baseUrl, {
    method: 'POST',
    mode: 'cors',
    cache: 'no-cache',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      UserName: userName,
      Password: password,
    }),
  }).then((res) => res.json());
  return response;
}

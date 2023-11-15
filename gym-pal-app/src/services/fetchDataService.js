import axios from 'axios';

const baseUrl = 'https://localhost:44323';

export function getSupplements() {
  const getUrl = `${baseUrl}/api/supplements`;

  return axios
    .get(getUrl)
    .then((response) => {
      if (response.status !== 200) {
        throw new Error(`Failed to fetch data (HTTP ${response.status})`);
      }
      return response.data;
    });
}

export function createSupplements(supplementData) {
  const postUrl = `${baseUrl}/api/supplements/CreateSupplement`;

  return axios
    .post(postUrl, supplementData)
    .then((response) => {
      if (response.status !== 200) {
        throw new Error(`Failed to create supplement (HTTP ${response.status})`);
      }
      return response.data; // This should be the created supplement from the API
    });
}

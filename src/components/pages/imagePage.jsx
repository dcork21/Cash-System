import React from 'react';
import styled from 'styled-components';

const ContentArea = styled.div`
  border: 1px solid green;
  display: flex;
  flex-direction: row;
  justify-content: space-evenly;
  height: auto;
  width: auto;
  box-sizing: border-box;
`;

function ImagePage(props) {
  return (
    <ContentArea>
      <img
        src='https://s.ftcdn.net/v2013/pics/all/curated/RKyaEDwp8J7JKeZWQPuOVWvkUjGQfpCx_cover_580.jpg?r=1a0fc22192d0c808b8bb2b9bcfbf4a45b1793687'
        alt='Nice Scene'
      />
    </ContentArea>
  );
}

export default ImagePage;

import React from 'react';
import styled from 'styled-components';

const ContentArea = styled.div`
  border: 1px solid white;
  border-radius: 5px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  height: auto;
  width: 100%;
  box-sizing: border-box;
  :hover {
    cursor: pointer;
  }
`;
const ButtonText = styled.div`
  font-size: 16px;
  width: 100%;
  text-align: center;
`;

function Button(props) {
  const { text, buttonKey, onClickFunc } = props;
  return (
    <ContentArea>
      <ButtonText onClick={() => onClickFunc(buttonKey)}>
        <p>{text}</p>
      </ButtonText>
    </ContentArea>
  );
}

export default Button;

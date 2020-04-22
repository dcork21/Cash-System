import React from 'react';
import styled from 'styled-components';

const ContentArea = styled.div`
  border: 1px solid white;
  border-radius: 5px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  height: 30px;
  width: 100%;
  box-sizing: border-box;
  margin: 5px;
  min-width: 40px;
  :hover {
    cursor: pointer;
    color: white;
  }
`;
const ButtonText = styled.div`
  font-size: 14px;
  width: 100%;
  text-align: center;
`;

function Button(props) {
  const { text, buttonKey, onClickFunc, onClickProps } = props;
  return (
    <ContentArea>
      <ButtonText onClick={() => onClickFunc(buttonKey, onClickProps)}>
        <p>{text}</p>
      </ButtonText>
    </ContentArea>
  );
}

export default Button;

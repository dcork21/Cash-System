import React from 'react';
import styled from 'styled-components';

const InputBox = styled.div`
  width: 80%;
  height: 40px;
  margin-top: 10px;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: auto;
  display: flex;
  justify-content: center;
  box-sizing: border-box;
`;
const InputKey = styled.div`
  width: 30%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  text-align: right;
  border-bottom: 1px solid white;
  padding-right: 5px;
  font-size: 18px;
  box-sizing: border-box;
`;
const InputValue = styled.input`
  width: 30%;
  font-size: 18px;
  border: 1px solid white;
  box-sizing: border-box;
`;
const InputWarning = styled.div`
  width: 10px;
  color: red;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  text-align: right;
  border-bottom: 1px solid white;
  font-size: 18px;
  box-sizing: border-box;
`;
export default function FormInput(props) {
  const {
    showWarning,
    inputKey,
    inputValue,
    inputType,
    updateFunction,
  } = props;

  return (
    <InputBox>
      <InputKey>{inputKey}</InputKey>
      <InputWarning>{showWarning ? '*' : ' '}</InputWarning>
      <InputValue
        type={inputType}
        onChange={(e) => updateFunction(e.target.value)}
        value={inputValue}
      />
    </InputBox>
  );
}

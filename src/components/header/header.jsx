import React from 'react';
import styled from 'styled-components';

const ContentArea = styled.div`
  position: relative;
  width: 100%;
  height: 100px;
  background-color: #edece8;
  box-sizing: border-box;
  display: flex;
  justify-content: center;
`;

const HeaderTitle = styled.div`
  font-size: 45px;
  font-family: ${'Calibri (Body)'};
`;

function Header(props) {
  return (
    <ContentArea>
      <HeaderTitle>The Smart Bank</HeaderTitle>
    </ContentArea>
  );
}

export default Header;

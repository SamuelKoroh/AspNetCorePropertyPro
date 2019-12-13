import React, {Fragment} from 'react';

const Dashboard = () => {
    return (
        <Fragment>

    <main>
      <div class="container" id="mainContainer">
        <p class=" lead text-success" id="welcomeUser"></p>
        <div class="mb-10 mt-30">
          <span class="lead text-primary" id="titleSpan">Manage Properties</span>
        </div>
      </div>
    </main>
    <footer>
      <p>All right reserved PropertyPro &copy; 2019</p>
    </footer>
        </Fragment>
    )
}

export default Dashboard

import { createRoot } from 'react-dom/client';

import 'bulma/css/bulma.css';
import '@fortawesome/fontawesome-free/css/all.css';

import { Root } from './Root';
import { Provider } from 'react-redux';
import { store } from './app/store';

createRoot(document.getElementById('root') as HTMLDivElement).render(
  <Provider store={store}>
    <Root />
  </Provider>
);
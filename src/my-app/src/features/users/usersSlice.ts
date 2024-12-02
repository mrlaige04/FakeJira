import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { User } from '../../types/User';
import { getUsers } from '../../api/users';

type UsersState = {
  users: User[];
  selectedUser: User | null;
  loading: boolean;
  error: string;
};

const initialState: UsersState = {
  users: [],
  selectedUser: null,
  loading: false,
  error: '',
};

const usersSlice = createSlice({
  name: 'users',
  initialState,
  reducers: {
    addUser: (state, action: PayloadAction<User>) => {
      state.users.push(action.payload);
    },
    updateUser: (state, action: PayloadAction<User>) => {
      const indexUser = state.users.findIndex(user => user.id === action.payload.id);

      state.users.splice(indexUser, 1, action.payload);
    },
    deleteUser: (state, action: PayloadAction<User>) => {
      const indexUser = state.users.findIndex(user => user.id === action.payload.id);

      state.users.splice(indexUser, 1);
    },
    addSelectedUser: (state, action: PayloadAction<User | null>) => {
      state.selectedUser = action.payload;
    },
  },
  extraReducers: builder => {
    builder.addCase(init.pending, state => {
      state.loading = true;
    });

    builder.addCase(init.fulfilled, (state, action) => {
      state.users = action.payload;
      state.loading = false;
    });

    builder.addCase(init.rejected, state => {
      state.error = 'Something went wrong!';
      state.loading = false;
    });
  },
});

export default usersSlice;
export const { addUser, updateUser, deleteUser, addSelectedUser } = usersSlice.actions;

export const init = createAsyncThunk('users/fetch', () => {
  return getUsers();
});

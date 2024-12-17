export interface LoginForm {
  email: string;
  password: string;
}

export interface LoginError {
  message: string;
  code?: string;
}

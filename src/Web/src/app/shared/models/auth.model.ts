// src/Web/src/app/shared/models/auth.model.ts
export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  fullName: string;
}

export interface AuthResponse {
  accessToken: string;
  refreshToken: string;
  accessTokenExpiresAt: string;
}

export interface DecodedToken {
  sub: string;
  email: string;
  role: 'Admin' | 'Member';
  exp: number;
}
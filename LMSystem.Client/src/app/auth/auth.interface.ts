export interface TokenResp {
    token: string;
    expiration: string;
}

export interface TokenResponse {
    token: TokenResp;
    
    role: string;
}
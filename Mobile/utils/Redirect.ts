export const forbiddenRedirect = 'Forbidden';
export const unauthenticatedRedirect = 'Login';
export const sleep = (milliseconds: number) => {
    return new Promise(resolve => setTimeout(resolve, milliseconds))
}
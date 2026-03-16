export class ErrorMessages {
  static readonly messages: { [key: string]: string } = {
    invalidUrl: 'Please enter a valid URL.',
    required: 'This field is required',
    minlength: `Minimum length is ${0}`,
    maxlength: `Maximum length is ${0}`,
    pattern: 'Invalid format',
    default: 'Invalid value',
  };

  static getErrorMessage(errorKey: string): string {
    return this.messages[errorKey] || 'Invalid input.';
  }
}

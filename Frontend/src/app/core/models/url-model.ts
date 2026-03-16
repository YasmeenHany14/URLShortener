import {BaseRequestModel, BaseResponseModel} from './base-model';

export interface GenerateUrlRequestModel extends BaseRequestModel {
  originalUrl: string;
}

export interface GenerateUrlResponseModel extends BaseResponseModel {
  shortUrlCode: string;
}

export interface RedirectUrlRequestModel extends BaseRequestModel {
  code: string;
}

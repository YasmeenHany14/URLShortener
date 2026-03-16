import {Injectable} from '@angular/core';
import {BaseService} from './base-service';
import {routes} from '../constants/routs';
import {GenerateUrlRequestModel, GenerateUrlResponseModel, RedirectUrlRequestModel} from '../models/url-model';
import {Observable} from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class UrlService extends BaseService{
  protected override route: string;
  constructor() {
    super();
    this.route = routes.url;
  }

  GenerateUrl(model: GenerateUrlRequestModel) {
    return this.httpClient.post<GenerateUrlResponseModel>(this.API_URL + this.route, model);
  }

  RedirectUrl(model: RedirectUrlRequestModel) {
    return this.httpClient.get(this.API_URL + this.route + '/' + model.code)
  }
}

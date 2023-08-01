import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import configurl from '../../assets/config/config.json';
import { BehaviorSubject, Observable, tap } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class BranchServiceService {
   private authUrl = configurl.apiServer.url;

  constructor(private httpClient: HttpClient) {}
  // getAllBranches() {
  //   return this.httpClient.get(this.authUrl + '/Branch');
  // }

  // private branchesSubject: BehaviorSubject<any[]> = new BehaviorSubject<any[]>([]);

  // public branches$: Observable<any[]> = this.branchesSubject.asObservable();


  // public getAllBranches(): void {
  //   this.httpClient.get<any[]>(this.authUrl + '/Branch').subscribe(
  //     (branches: any[]) => {
  //       this.branchesSubject.next(branches);
  //     },
  //     (error) => {
  //       console.error('Failed to fetch branches:', error);
  //     }
  //   );
  // }

  branches: any = []

  branchesSubject: BehaviorSubject<any[]> = new BehaviorSubject(this.branches);

  getAllBranches(): Observable<any[]> {
    return this.httpClient.get<any[]>(this.authUrl + '/Branch');
  }

  refreshBranches() {
    return this.branchesSubject;
  }

  // addBranch(data:any){
  //   console.log(data);
  //   return this.httpClient.post(this.authUrl + '/Branch',data);
  // }

  addBranch(data: any): Observable<any> {
    return this.httpClient.post<any>(this.authUrl + '/Branch', data)
        .pipe(
            tap(() => {
              this.branchesSubject.next(this.branches);
          })
      )
  }

  getBranchByName(data:any) {
    return this.httpClient.get(this.authUrl + '/Branch/'+data);
  }
}

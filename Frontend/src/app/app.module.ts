import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatDialogModule } from '@angular/material/dialog';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { HomePageComponent } from './home-page/home-page.component';
import { SignupComponent } from './signup/signup.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { AboutComponent } from './about/about.component';
import { ServiceeComponent } from './servicee/servicee.component';
import { FooterComponent } from './footer/footer.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FundTransferComponent } from './fund-transfer/fund-transfer.component';
import { FundTransferListComponent } from './fund-transfer-list/fund-transfer-list.component';
import { BranchComponent } from './branch/branch.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatTableModule } from '@angular/material/table';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { HomeComponent } from './home/home.component';
import { UnusualTransactionAlertComponent } from './unusual-transaction-alerts/unusual-transaction-alerts.component';
import { RecentActivityComponent } from './recent-activity/recent-activity.component';
import { TransactionsChartComponent } from './transactions-chart/transactions-chart.component';
import { TransactionsComponent } from './transactions/transactions.component';
import { ClientProfileComponent } from './client-profile/client-profile.component';
import { ManagerComponent } from './manager/manager.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AuthGuard } from './guards/auth.guard';
import { JwtHelperService } from '@auth0/angular-jwt';
//import { ApplyLoanComponent } from './apply-loan/apply-loan.component';
import { ApproveLoanComponent } from './approve-loan/approve-loan.component';
import { SelectLoanComponent } from './select-loan/select-loan.component';
import { ToastrModule } from 'ngx-toastr';
@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomePageComponent,
    SignupComponent,
    LoginComponent,
    AboutComponent,
    ServiceeComponent,
    FooterComponent,
    DashboardComponent,
    FundTransferComponent,
    FundTransferListComponent,
    BranchComponent,
    AppComponent,
    HomeComponent,
    UnusualTransactionAlertComponent,
    RecentActivityComponent,
    TransactionsChartComponent,
    TransactionsComponent,
    ClientProfileComponent,
    ManagerComponent,
    ApproveLoanComponent,
    SelectLoanComponent,
    
   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
   BrowserAnimationsModule,
   ToastrModule.forRoot(),
   HttpClientModule,
   FormsModule,
   ReactiveFormsModule,
   MatToolbarModule,
   MatIconModule,
   MatCardModule,
   MatMenuModule,
   MatTableModule,
   NgxChartsModule,
   NgbModule,
   MatDialogModule,
   
  ],
  providers: [AuthGuard,JwtHelperService],
  bootstrap: [AppComponent]
})
export class AppModule { }

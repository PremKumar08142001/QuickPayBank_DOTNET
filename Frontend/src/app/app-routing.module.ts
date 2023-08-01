import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { HomePageComponent } from './home-page/home-page.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FundTransferComponent } from './fund-transfer/fund-transfer.component';
import { BranchComponent } from './branch/branch.component';
import { HomeComponent } from './home/home.component';
import { TransactionsComponent } from './transactions/transactions.component';
import { ManagerComponent } from './manager/manager.component';
import { AuthGuard } from './guards/auth.guard';
import { SelectLoanComponent } from './select-loan/select-loan.component';
import { ApproveLoanComponent } from './approve-loan/approve-loan.component';

const routes: Routes = [
  // {path:"",redirectTo:"loginsignup", pathMatch:"full"},
{path:"",redirectTo: '/home', pathMatch: 'full'},
{path:"login",component:LoginComponent},
{path:"signup",component:SignupComponent},
{path:"home",component:HomePageComponent},
{path:"dashboard",component:DashboardComponent},
{path:"transfer",component:FundTransferComponent},
{path:"branch",component:BranchComponent},
{path:"monitoring",component:HomeComponent,canActivate:[AuthGuard]},
{path:"profile/:userName",component:TransactionsComponent},
{path:"manager",component:ManagerComponent},
{path:"loan",component:SelectLoanComponent},
{path:"approve",component:ApproveLoanComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

import { Component, OnInit } from '@angular/core';
import { Color, ScaleType } from '@swimlane/ngx-charts';
import { Subscription, delay, interval } from 'rxjs';
import { FundTransfer } from '../models/FundTransfer';
import { FundTransferService } from '../services/fund-transfer.service';

interface GroupedTransaction {
  transactionDate: string;
  groups: {
    type: string;
    count: number;
  }[];
}

@Component({
  selector: 'app-transactions-chart',
  templateUrl: './transactions-chart.component.html',
  styleUrls: ['./transactions-chart.component.css']
})

export class TransactionsChartComponent implements OnInit {
   
  data: any[] = []; 
  legend: any[]; 

  groupedTransactions: any;

  customColorSchemeBar: Color =
    {
      name: 'customColorScheme',
      selectable: true,
      group: ScaleType.Ordinal,
     // domain: ['#98FB98', '#FF6B6B']
     // domain: ['#00ff00', '#ff0000']
     domain: ['#00C853', '#FF1744']
     // domain: ['#FF1744', '#00C853']
    };

  customColorScheme: Color =
    {
      name: 'customColorScheme',
      selectable: true,
      group: ScaleType.Ordinal,
     // domain: ['#98FB98', '#FF6B6B']
     // domain: ['#00ff00', '#ff0000']
      domain: ['#00C853', '#FF1744']
     // domain: ['#FF1744', '#00C853']
    };

  tdata = [
    // {
    //   "name": "00:00",
    //   "series": [
    //     {
    //       "name": "Complete",
    //       "value": 25 
    //     },
    //     {
    //       "name": "Fraud",
    //       "value": 10 
    //     }
    //   ]
    // }
  ];
  
  legitPercentage: Number = 0;
  fraudPercentage: Number = 0;

  pieData = [
      {
        name: 'Legitimate',
        value: this.legitPercentage
      },
      {
        name: 'Fraudulent',
        value: Number(this.fraudPercentage?.toFixed(0))
      }
  ];
  

  constructor(private transactionService: FundTransferService) {
    // this.data = this.tdata;
    this.legend = [
      { label: 'Legitimate', color: '#2ecc71' },
      { label: 'Fraudulent', color: '#e74c3c' }
    ];
    // this.colorHelper = new ColorHelper('ordinal', this.legend.map(item => item.color), 'ordinal', ['#ffffff']);
    // this.colorScheme = new ColorHelper(schemeCategory10, 'ordinal', this.legend.map(item => item.color));
    
  }

  private subscription!: Subscription;

  _tdata!: any[]

  ngOnInit() {
    // this.getTransactions();
    // this.transactionService.transactionData.subscribe(data => {
    //   console.log('subjectData ', data);
    //   this.getTransactions();
    // }); 
    this.subscription = interval(7000).subscribe(() => {
      this._tdata = this.getTransactions();
      this.transactionService.updateTransactionData(this._tdata);
    });

    this.subscription.add(
      this.transactionService.transactionData$.pipe(delay(6000)).subscribe(newData => {
        this.data = newData;
      })
    );
  }

  
  groupTransactions(transactions: FundTransfer[]): any {
    
    const groupedData: any[] = [];
  
    // Group transactions by transaction date
  const groupedByDate = transactions.reduce((groups: any, transaction: any) => {
    const transactionDate = transaction.transactionDate;

    if (!groups[transactionDate]) {
      groups[transactionDate] = [];
    }

    groups[transactionDate].push(transaction);

    
    return groups;
  }, {});
  
  // Iterate over the groupedByDate object
  for (const transactionDate in groupedByDate) {
    if (groupedByDate.hasOwnProperty(transactionDate)) {
      const transactionsForDate = groupedByDate[transactionDate];
      // Group transactions by type and count the occurrences
      const groupedByType = transactionsForDate.reduce((groups: any, transaction: any) => {
        
        const type = transaction.status;
        
        console.log('groups ', groupedByDate);
        
        if (!groups[type]) {
          groups[type] = 0;
        }
        
        groups[type]++;
        
        return groups;
      }, {});
      
      const groups: { type: string; count: number }[] = [];
      
      // Iterate over the groupedByType object
      console.log('date ', groupedByDate);
      for (const type in groupedByType) {
        if (groupedByType.hasOwnProperty(type)) {
          groups.push({ type, count: groupedByType[type] });
        }
      }

      groupedData.push({ transactionDate, groups });
    }
  }

    console.log("groupedData ", groupedData);
    
    return groupedData;
}
  
  

  getTransactions(): any {
    this.transactionService.getTransaction().subscribe(transactions => {
          
        const total = transactions.length
    
        const legitTransactions = (transactions.filter((transition: { status: any; }) => 
        transition.status == 'Completed')).length
    
        const fraudTransactions = (transactions.filter((transition: { status: any; }) => 
        transition.status == 'Suspicious')).length
    
        this.legitPercentage =  (legitTransactions / total) * 100;
    
        this.fraudPercentage =  (fraudTransactions / total) * 100;
        
        console.log('transactions ', transactions, this.legitPercentage, this.fraudPercentage);

        // const dateObj = new Date(transactions.transactionDate);
        const dateObj = new Date("2023-06-28 14:13:46.1941115");

        const hours = dateObj.getHours();

        const minutes = dateObj.getMinutes();

        const transitionDate = `${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}`;

        console.log('transitionDate ', transitionDate, transactions[0].transactionDate);
    
        this.pieData = [
          {
            name: 'Legitimate',
            value: Number(this.legitPercentage?.toFixed(0))
          },
          {
            name: 'Suspicious',
            value: Number(this.fraudPercentage?.toFixed(0))
          }
        ]
    
        // this.data = [{
        //     "name": "12:00",
        //     "series": [
        //       {
        //         "name": "Complete",
        //         "value": (transactions.filter((transaction: { status: any; }) => 
        //                           transaction.status == 'Completed')).length
        //       },
        //       {
        //         "name": "Suspicious",
        //         "value": (transactions.filter((transaction: { status: any; }) => 
        //                           transaction.status == 'Suspicious')).length
        //       }
        //     ]
        //   }]
          this.groupedTransactions = this.groupTransactions(transactions);
          this.transformData(transactions, this.groupedTransactions);
          return this.data;
        })
      }

      transformData(transactions: any, groupedTransactions: any) {

        const transformedData = groupedTransactions.map((group: { transactionDate: number ; groups: any[]; }) => {
            
            const timestamp = new Date(group.transactionDate);

            // let transactionDate = timestamp.getUTCHours() + ':00';

            const formattedDate = new Date(group.transactionDate);

            const transactionDate = formattedDate.toLocaleString('en-US', { hour: 'numeric', hour12: true });

            // console.log('transactionDate ', transactionDate, timestamp, group);
      
            return {
            name: transactionDate,
            series: group.groups.map((typeGroup: { type: string; }) => {
              
              const completeCount = transactions.filter((transaction: { transactionDate: any; type: any; status: string; }) =>
                transaction.transactionDate === group.transactionDate && transaction.status === typeGroup.type &&
                transaction.status === 'Completed'
              ).length;
      
            const fraudCount = transactions.filter((transaction: { transactionDate: any; type: any; status: string; }) =>
              transaction.transactionDate === group.transactionDate && transaction.status === typeGroup.type &&
              transaction.status === 'Suspicious'
            ).length;
      
            return {
                name: typeGroup.type,
                value: typeGroup.type === 'Completed' ? completeCount : fraudCount
              };
            })
          };
        });
        console.log("transformedData ", transformedData);
        this.data = transformedData;
      }
      

}

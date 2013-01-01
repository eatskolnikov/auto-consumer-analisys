//#include<iostream>
//#include<vector>
//
//using namespace std;
//
//int main()
//{
//	int n;
//	int a;
//	int chest=0, biceps=0, back=0;
//	cin >> n;
//	for(int i=0;i<n;++i)
//	{
//		cin >> a;
//		int curr = i%3;
//		switch(curr)
//		{
//		case 0:
//			chest+=a;
//			break;
//		case 1:
//			biceps+=a;
//			break;
//		case 2:
//			back+=a;
//			break;
//		}
//	}
//	if(chest > biceps && chest > back)
//		cout << "chest" << endl;
//	else if(biceps > chest && biceps > back)
//		cout << "biceps" << endl;
//	else
//		cout << "back" << endl;
//	return 0;
//}
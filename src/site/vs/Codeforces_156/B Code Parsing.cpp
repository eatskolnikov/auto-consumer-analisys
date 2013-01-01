//#include<iostream>
//#include<vector>
//#include<string>
//#include<cstring>
//
//using namespace std;
//
//int main()
//{
//	string s;
//	cin >> s;
//	int l = s.length();
//	int coount[2] = {0, 0};
//	for(int i=0;i<l;++i)
//		++coount[s[i] - 'x'];
//	if(coount[0] > coount[1])
//	{
//		int limit = coount[0] - coount[1];
//		for(int i=0;i<limit; ++i)
//			cout << "x";
//	}else{
//		int limit = coount[1] - coount[0];
//		for(int i=0;i<limit; ++i)
//			cout << "y";
//	}
//	cout << endl;
//	return 0;
//}